using System;
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
            EnterRoom(null, new Room(game, currentRoom));
        }

        internal void EnterRoom(DoorTile door, Room room)
        {
            if (room == null)
                room = new Room(game, currentRoom);

            if (!rooms.Contains(room))
                this.rooms.Add(room);

            this.currentRoom = room;
        }
    }
}