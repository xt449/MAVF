using Microsoft.AspNetCore.SignalR;
using MILAV.API.Connection;
using MILAV.API.Device;

namespace MILAV
{
    public class SignalRHub : Hub
    {
        protected readonly Controller controller;

        public SignalRHub(Controller controller)
        {
            this.controller = controller;
        }

        public AbstractDevice[] GetDevices()
        {
            return controller.GetDevices();
        }

        public AbstractDevice? GetDeviceById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device;
        }

        public string? GetDeviceDriverById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.Driver;
        }

        public string? GetDeviceIpById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.Ip;
        }

        public int? GetDevicePortById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.Port;
        }

        public Protocol? GetDeviceProtocolById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.Protocol;
        }

        public string? GetDeviceRoomById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.Room;
        }

        public ControlState? GetDeviceStateById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.State;
        }

        public ControlState[]? GetDeviceStatesById(string id)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.States;
        }

        public string[]? GetDeviceStateRoomsById(string id, string state)
        {
            controller.TryGetDevice(id, out AbstractDevice? device);
            return device?.States.FirstOrDefault(s => s.Id == state)?.Rooms;
        }
    }
}
