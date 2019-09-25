using System;

namespace Labb4
{
    public class Room
    {
        private readonly int mapHeight;
        private readonly int mapWidth;
        private readonly Game game;

        public Tile[,] Map { get; private set; }

        public Room(Game game)
        {
            this.game = game;
            this.mapWidth = game.MapWidth;
            this.mapHeight = game.MapHeight;
        }

        public void Generate(Room previousRoom)
        {
            this.Map = new Tile[mapWidth, mapHeight];
            int playerX = game.Player.PosX;
            int playerY = game.Player.PosY;

            for (int row = 0; row < this.mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    // When player enters new room, add a ReturnDoorTile at their position
                    if (row == playerX && column == playerY && previousRoom != null)
                    {
                        Map[row, column] = new ReturnDoorTile(previousRoom);
                    }
                    else if (row == 0 || column == 0 || row == mapWidth - 1 || column == mapHeight - 1)
                    {
                        if ((row == 0 && column == 0) ||
                            (row == 0 && column == mapHeight - 1) ||
                            (row == mapWidth - 1 && column == 0) ||
                            (row == mapWidth - 1 && column == mapHeight - 1))
                        {
                            Map[row, column] = new CornerTile();
                        }
                        else
                        {
                            Map[row, column] = new WallTile();
                        }
                    }
                    else
                    {
                        Map[row, column] = new RoomTile();
                    }
                }
            }

            int nrOfTraps = new Random().Next(3, game.MapHeight);
            int nrOfMonsters = new Random().Next(3, game.MapWidth);

            PlaceKeyDoorPair(new DoorTile(game, ConsoleColor.Red));
            PlaceKeyDoorPair(new DoorTile(game, ConsoleColor.Blue));
            PlaceButtonTrapPairs(nrOfTraps);
            PlaceMonsters(nrOfMonsters);
        }

        private void PlaceKeyDoorPair(DoorTile door)
        {
            PlaceTile(new KeyTile(door), typeof(RoomTile));
            PlaceTile(door, typeof(WallTile));
        }

        private void PlaceButtonTrapPairs(int pairs)
        {
            for (int i = 0; i < pairs; i++)
            {
                TrapTile trap = new TrapTile();
                PlaceTile(new ButtonTile(trap), typeof(RoomTile));
                PlaceTile(trap, typeof(RoomTile));
            }
        }

        private void PlaceMonsters(int monsters)
        {
            for (int i = 0; i < monsters; i++)
            {
                PlaceTile(new MonsterTile(), typeof(RoomTile));
            }
        }

        private bool PlaceTile(Tile newTile, Type oldTile)
        {
            bool[,] visited = new bool[Map.GetLength(0), Map.GetLength(1)];
            int unvisitedCount = Map.GetLength(0) * Map.GetLength(1);
            while (unvisitedCount > 0)
            {
                int posX = new Random().Next(0, this.mapWidth);
                int posY = new Random().Next(0, this.mapHeight);

                if (visited[posX, posY] != true)
                {
                    if (Map[posX, posY].GetType() == oldTile &&
                        (posX != game.Player.PosX ||
                        posY != game.Player.PosY))
                    {
                        Map[posX, posY] = newTile;
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
