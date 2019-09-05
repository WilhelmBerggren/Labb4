using System.Collections.Generic;

namespace Labb4
{
    class Level
    {
        public Tile[,] map;
        public int mapHeight;
        public int mapWidth;
        public Level(int mapHeight, int mapWidth)
        {
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
            this.map = new Tile[mapHeight, mapWidth];
            SetTiles();
        }


        char[,] room = new char[,] {
           { '#', '#', '#', '#', '#'},
           { '#', '.', '.', '.', '#'},
           { '#', '.', 'M', '.', '#'},
           { '#', '.', '.', 'D', '#'},
           { '#', '#', '#', '#', '#'},
       };
        Tile[,] Parser(char[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            Tile[,] output = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    switch (input[i, j])
                    {
                        case 'D':
                            output[i, j] = new DoorTile();
                            break;
                        case '#':
                            output[i, j] = new WallTile();
                            break;
                        case '.':
                            output[i, j] = new RoomTile();
                            break;
                        case 'M':
                            output[i, j] = new MonsterTile();
                            break;
                        default:
                            output[i, j] = new RoomTile();
                            break;
                    }
                }
            }
            return output;
        }
        public void SetTiles()
        {
            this.map = Parser(room);
            this.mapHeight = this.map.GetLength(0);
            this.mapWidth = this.map.GetLength(1);
            //for (int x = 0; x < this.mapWidth; x++)
            //{
            //    for (int y = 0; y < this.mapHeight; y++)
            //    {
            //        if (x == 3 && y == 4)
            //            map[x, y] = new MonsterTile();
            //        else if (x == 0 || y == 0 || x == this.mapHeight - 1 || y == this.mapWidth - 1)
            //            map[x, y] = new WallTile();
            //        else
            //            map[x, y] = new RoomTile();
            //    }
            //}
        }
    }
}