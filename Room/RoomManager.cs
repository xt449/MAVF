using System.Collections.Generic;

namespace MILAV.Room
{
    public class RoomManager
    {
        private readonly Dictionary<string, IRoom> rooms = new Dictionary<string, IRoom>();

        public RoomManager()
        {
        }

        public bool AddRoom(IRoom room)
        {
            if (rooms.ContainsKey(room.Id))
            {
                return false;
            }

            rooms[room.Id] = room;

            return true;
        }

        public bool TryGetRoom(string id, out IRoom room)
        {
            return rooms.TryGetValue(id, out room);
        }

        public IRoom GetRoom(string id)
        {
            return rooms[id];
        }
    }
}
