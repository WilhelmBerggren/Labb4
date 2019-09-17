using System;

namespace Labb4
{
    public class Room
    {
        public Tile[,] map;
        public int mapHeight;
        public int mapWidth;
        public Room(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.map = new Tile[mapWidth, mapHeight];
            Generate();
        }
        public void Generate()
        {
            for (int row = 0; row < this.mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    if (row == 0 || column == 0 || row == mapWidth - 1 || column == mapHeight - 1)
                        if ((row == 0 && column == 0) ||
                            (row == 0 && column == mapHeight - 1) ||
                            (row == mapWidth - 1 && column == 0) ||
                            (row == mapWidth - 1 && column == mapHeight - 1))
                        {
                            map[row, column] = new CornerTile();
                        }
                        else
                            map[row, column] = new WallTile();
                    else
                        map[row, column] = new RoomTile();
                }
            }
            PlaceKeyDoorPair(new DoorTile(ConsoleColor.Red));
            PlaceKeyDoorPair(new DoorTile(ConsoleColor.Blue));
            PlaceButtonTrapPairs(1);
            PlaceMonsters(1);
        }
        public void PlaceMonsters(int monsters)
        {
            for (int i = 0; i < monsters; i++)
            {
                PlaceTile(new MonsterTile(), typeof(RoomTile));
            }
        }

        public void PlaceKeyDoorPair(DoorTile door)
        {
            PlaceTile(new KeyTile(door), typeof(RoomTile));
            PlaceTile(door, typeof(WallTile));
        }

        public void PlaceButtonTrapPairs(int pairs)
        {
            for (int i = 0; i < pairs; i++)
            {
                TrapTile trap = new TrapTile();
                PlaceTile(new ButtonTile(trap), typeof(RoomTile));
                PlaceTile(trap, typeof(RoomTile));
            }
        }

        public bool PlaceTile(Tile newTile, Type oldTile)
        {
            bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)];
            int unvisitedCount = map.GetLength(0) * map.GetLength(1);
            while (unvisitedCount > 0)
            {
                int posX = new Random().Next(0, this.mapWidth);
                int posY = new Random().Next(0, this.mapHeight);

                if (visited[posX, posY] != true)
                {
                    if (map[posX, posY].GetType() == oldTile)
                    {
                        map[posX, posY] = newTile;
                        return true;
                    }
                }
                else
                {
                    visited[posX, posY] = true;
                    unvisitedCount--;
                }
            }
            return false;
        }
    }
}
