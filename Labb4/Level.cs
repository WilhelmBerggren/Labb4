using System.Collections.Generic;

namespace Labb4
{
    public class Level
    {
        public Tile[,] map;
        public int mapHeight;
        public int mapWidth;
        public Dictionary<KeyTile, DoorTile> keyDoorDict;
        public Level(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.map = new Tile[mapWidth, mapHeight];
            this.keyDoorDict = new Dictionary<KeyTile, DoorTile>();
            SetTiles();
        }


        string[,] room = new string[,] {
           { "#", "#", "#", "#", "#"},
           { "#", "K1", ".", ".", "#"},
           { "#", ".", "M", ".", "#"},
           { "#", ".", ".", "D1", "#"},
           { "#", "#", "#", "#", "#"},
       };
        Tile[,] Parser(string[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            Tile[,] output = new Tile[width, height];
            Dictionary<int, KeyTile> keys = new Dictionary<int, KeyTile>();
            Dictionary<int, DoorTile> doors = new Dictionary<int, DoorTile>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    string s = input[i, j];
                    switch (s)
                    {
                        case "#":
                            output[i, j] = new WallTile();
                            break;
                        case ".":
                            output[i, j] = new RoomTile();
                            break;
                        case "M":
                            output[i, j] = new MonsterTile();
                            break;
                        default:
                            if(s.StartsWith("D"))
                                doors.Add(int.Parse(s.Substring(1)), new DoorTile());
                            else if(s.StartsWith("K"))
                                keys.Add(int.Parse(s.Substring(1)), new KeyTile());
                            output[i, j] = new RoomTile();
                            break;
                    }
                }
            }

            foreach(KeyValuePair<int, KeyTile> key in keys) {
                this.keyDoorDict.Add(key.Value, doors[key.Key]);
            }
            return output;
        }
        public void SetTiles()
        {
            //this.map = Parser(room);
            this.mapWidth = this.map.GetLength(0);
            this.mapHeight = this.map.GetLength(1);
            for (int width = 0; width < this.mapWidth; width++)
            {
                for (int height = 0; height < this.mapHeight; height++)
                {
                    if (width == 3 && height == 4)
                        map[width, height] = new MonsterTile();
                    else if (width == 0 || height == 0 || width == this.mapWidth - 1 || height == this.mapHeight - 1)
                        map[width, height] = new WallTile();
                    else
                        map[width, height] = new RoomTile();
                }
            }
        }
    }
}