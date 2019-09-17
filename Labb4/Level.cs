using System;
using System.Collections.Generic;

namespace Labb4
{
    public class Level
    {
        public List<Room> rooms;
        public Tile[,] map;
        public int mapHeight;
        public int mapWidth;
        public Level(int mapWidth, int mapHeight)
        {
            rooms = new List<Room>();
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            EnterNewRoom();
        }
        public void EnterNewRoom()
        {
            Room room = new Room(mapWidth, mapHeight);
            this.rooms.Add(room);
            this.map = room.map;
        }
    }
}