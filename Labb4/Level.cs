using System.Collections.Generic;

namespace Labb4
{
    public class Level
    {
        public Game game;
        public List<Room> rooms;
        public Room currentRoom;

        public Level(Game game)
        {
            this.game = game;
            rooms = new List<Room>();
            Room room = new Room(game);
            EnterRoom(room);
        }

        internal void EnterRoom(Room room)
        {
            if (room.Map == null)
            {
                room.Generate(this.currentRoom);
            }
            if (!rooms.Contains(room))
            {
                this.rooms.Add(room);
            }

            this.currentRoom = room;
        }
    }
}