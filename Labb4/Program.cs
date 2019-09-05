using System;

namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible to step on, meaning if you can go to its position
    public class Tile
    {
        public char representation;
        public bool accessible;
    }

    public class RoomTile : Tile
    {
        public RoomTile()
        {
            this.representation = '.';
            this.accessible = true;
        }
    }

    public class DoorTile : Tile
    {
        public DoorTile()
        {
            this.representation = 'D';
            this.accessible = true;
        }
    }

    public class WallTile : Tile
    {
        public WallTile()
        {
            this.representation = '#';
            this.accessible = false;
        }
    }

    public class MonsterTile : Tile
    {
        public MonsterTile()
        {
            this.representation = 'M';
            this.accessible = true;
        }
    }

    public class Player
    {
        public int PosX;
        public int PosY;
        public int lives;
        public Player(int posX, int posY, int lives)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.lives = lives;
        }

        public void move(Tile t, int deltaX, int deltaY)
        {
            if (t.accessible)
            {
                this.PosX += deltaX;
                this.PosY += deltaY;
                if (t.GetType() == typeof(MonsterTile)) lives--;
            }
        }
    }


    public class Game
    {
        const int mapHeight = 10;
        const int mapWidth = 10;
        Level level = new Level(mapHeight, mapWidth);
        Player player = new Player(5, 5, 10);

        public void Start()
        {
            while (true)
            {
                Draw();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        player.move(level.map[player.PosX - 1, player.PosY], -1, 0);
                        break;
                    case ConsoleKey.A:
                        player.move(level.map[player.PosX, player.PosY - 1], 0, -1);
                        break;
                    case ConsoleKey.S:
                        player.move(level.map[player.PosX + 1, player.PosY], +1, 0);
                        break;
                    case ConsoleKey.D:
                        player.move(level.map[player.PosX, player.PosY + 1], 0, +1);
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public void Draw()
        {
            Console.Clear();
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    char c = (player.PosX == x && player.PosY == y) ? 'X' : level.map[x, y].representation;
                    Console.Write(c + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"lives: {player.lives}");
        }
    }

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
            setTiles();
        }

        void setTiles()
        {
            for (int x = 0; x < this.mapWidth; x++)
            {
                for (int y = 0; y < this.mapHeight; y++)
                {
                    if (x == 3 && y == 4)
                        map[x, y] = new MonsterTile();
                    else if (x == 0 || y == 0 || x == this.mapHeight - 1 || y == this.mapWidth - 1)
                        map[x, y] = new WallTile();
                    else
                        map[x, y] = new RoomTile();
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}