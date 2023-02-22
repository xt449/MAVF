using MILAV.API.Device;
using System.Collections.Generic;

namespace MILAV.Room
{
    public class RoomManager
    {
        private readonly Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        public RoomManager()
        {
        }

        public Room AddRoom(string id, string name, IEnumerable<IDevice> devices)
        {
            if (rooms.ContainsKey(id))
            {
                return null;
            }

            var room = new Room(id, name, devices);
            rooms[id] = room;

            return room;
        }

        public bool TryGetRoom(string id, out Room room)
        {
            return rooms.TryGetValue(id, out room);
        }

        public Room GetRoom(string id)
        {
            return rooms[id];
        }
    }
}
