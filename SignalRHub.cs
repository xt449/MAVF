using Microsoft.AspNetCore.SignalR;
using MILAV.API;
using MILAV.API.Connection;
using MILAV.API.Device;

namespace MILAV
{
    public class SignalRHub : Hub, ServerAPI
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
        public string? GetDeviceDriverById(string id)
        {
            return controller.GetDeviceById(id)?.Driver;
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

        public string? GetDeviceRoomById(string id)
        {
            return controller.GetDeviceById(id)?.room;
        }

        public ControlState? GetDeviceStateById(string id)
        {
            return controller.GetDeviceById(id)?.State;
        }

        public ControlState[]? GetDeviceStatesById(string id)
        {
            return controller.GetDeviceById(id)?.States;
        }

        public string[]? GetDeviceStateRoomsById(string id, string state)
        {
            return controller.GetDeviceById(id)?.States.FirstOrDefault(cs => cs.id == state)?.rooms;
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
