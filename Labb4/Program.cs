using System;
using System.Collections.Generic;

namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible later on, meaning if you can go to its position
    // List<objects> contents. Wilhelm, wut is dis? Example, object Key?
    public class Tile
    {
        public char representation;
        public bool accessible;
        public List<object> contents;
    }

    public class RoomTile : Tile
    {
        public RoomTile()
        {
            this.representation = '.';
            this.accessible = true; // accessilbe Originates to the RoomTile(char representation) constructor below.
        }

        // Leave this for now, not needed as of current version
        //public RoomTile(char representation) // public constructor taking the representation as an argument and sets the representation of this class to the representation??
        //{                                   // this.representation is not needed, works anyway. this.accessible = true, sets this '.' to accessible.
        //    // this.representation = representation;
        //    this.accessible = true;
        //}
    }

    public class DoorTile : Tile
    {
        public DoorTile()
        {
            this.representation = 'D'; // sets the char representation to the char 'D' in this constructor
            //this.accessible = ? Key needed? If so, make method call to check if key collected. If collected, set accessible to true, otherwise false until key is collected.
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

    public class Player
    {
        public int PosX;
        public int PosY;
        public Player(int posX, int posY, int lives)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Lives = lives;
        }
        private int lives;
        public int Lives { get => lives; set => lives = value; }
        enum Dir { Up, Down, Left, Right }

        // Movement methods, if (CanMove(enum Dir.member)) change the value of the position variable and Redraws the map
        // Y-line has inverted values. Up = Y--, while in math Up is Y++
        public void moveUp(Tile t) { if (CanMove(Dir.Up)) this.PosY--; } // originally PosY++;
        public void moveDown() { if (CanMove(Dir.Down)) this.PosY++; } // originally PosX++;
        public void moveLeft() { if (CanMove(Dir.Left)) this.PosX--; } // originally PosY--;
        public void moveRight() { if (CanMove(Dir.Right)) this.PosX++; } // originally PoSY++;
        private bool CanMove(Player.Dir d)
        {
            switch (d)
            {
                case Dir.Up:
                    if ((t.)
                        return false;
                    else
                        return true;
                case Dir.Down:
                    if (PosY + 1.Equals())
                        return false;
                    else
                        return true;
                case Dir.Left:
                    if (PosX.Equals("#"))
                        return false;
                    else
                        return true;
                case Dir.Right:
                    if (PosX.Equals("#"))
                        return false;
                    else
                        return true;
            }
            return true;
        }
    }

    public class Game
    {
        const int mapX = 10;
        const int mapY = 10;
        Tile[,] map = new Tile[mapX, mapY];
        Player player = new Player(5, 5, 10);

        public void Start()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x == mapX - 1 || y == mapY - 1)
                        map[x, y] = new WallTile();
                    else
                        map[x, y] = new RoomTile();
                }
            }
            while (true)
            { 
                Console.Clear();
                Draw();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        player.moveUp();
                        break;
                    case ConsoleKey.A:
                        player.moveLeft();
                        break;
                    case ConsoleKey.S:
                        player.moveDown();
                        break;
                    case ConsoleKey.D:
                        player.moveRight();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public void Draw()
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    char c = (player.PosX == j && player.PosY == i) ? 'X' : map[i, j].representation;
                    Console.Write(c + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"lives: {player.Lives}");
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