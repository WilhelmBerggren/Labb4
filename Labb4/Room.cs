using System;

namespace Labb4
{
    public class Room
    {
        private Tile[,] map;
        private int mapHeight;
        private int mapWidth;

        public Tile[,] Map { get => map; }

        private Game game;
        public Room(Game game, Room previousRoom)
        {
            this.game = game;
            this.mapWidth = game.MapWidth;
            this.mapHeight = game.MapHeight;
        }
        public void Generate(Room previousRoom)
        {
            this.map = new Tile[mapWidth, mapHeight];
            int playerX = game.Player.PosX;
            int playerY = game.Player.PosY;

            for (int row = 0; row < this.mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    if (row == playerX && column == playerY && previousRoom != null)
                    {
                        map[row, column] = new ReturnDoorTile(previousRoom);
                    }
                    else if (row == 0 || column == 0 || row == mapWidth - 1 || column == mapHeight - 1)
                    {
                        if ((row == 0 && column == 0) ||
                            (row == 0 && column == mapHeight - 1) ||
                            (row == mapWidth - 1 && column == 0) ||
                            (row == mapWidth - 1 && column == mapHeight - 1))
                        {
                            map[row, column] = new CornerTile();
                        }
                        else
                            map[row, column] = new WallTile();
                    }
                    else
                        map[row, column] = new RoomTile();
                }
            }
            PlaceKeyDoorPair(new DoorTile(game, ConsoleColor.Red, this));
            PlaceKeyDoorPair(new DoorTile(game, ConsoleColor.Blue, this));
            PlaceButtonTrapPairs(1);
            int monsterCount = new Random().Next(3, game.MapWidth);
            PlaceMonsters(monsterCount);
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
                    if (map[posX, posY].GetType() == oldTile &&
                        posX != game.Player.PosX &&
                        posY != game.Player.PosY)
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
