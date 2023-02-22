using MILAV.API.Device;
using System;
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
            if(rooms.ContainsKey(id)) {
                return null;
            }

            var room = new Room(id, name, devices);
            rooms[id] = room;

            return room;
        }
    }
}
