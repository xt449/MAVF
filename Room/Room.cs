using MILAV.API.Device;
using System.Collections.Generic;

namespace MILAV.Room
{
    public class Room
    {
        private readonly string id;
        private readonly string name;

        // Only CONTROL type devices
        private readonly Dictionary<string, IDevice> devices = new Dictionary<string, IDevice>();

        public Room(string id, string name, IEnumerable<IDevice> devices)
        {
            this.id = id;
            this.name = name;

            foreach (var device in devices)
            {
                this.devices[device.Id] = device;
            };
        }

        public bool TryGetDevice(string id, out IDevice device)
        {
            return devices.TryGetValue(id, out device);
        }

        public IDevice GetDevice(string id)
        {
            return devices[id];
        }
    }
}
