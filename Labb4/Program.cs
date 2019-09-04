using System;
using System.Collections.Generic;

namespace Labb4
{
    public class Tile
    {
        public char representation;
        public bool accessible;
        public List<object> contents;
    }
    public class RoomTile : Tile
    {// Provar commit
        public RoomTile() { this.representation = '.'; }
        public RoomTile(char representation)
        {
            this.representation = representation;
            this.accessible = true;
        }
    }
    public class DoorTile : Tile
    {
        public DoorTile()
        {
            this.representation = 'D';
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
        public Tile currentTile;
        private int lives;
        public int Lives { get => lives; set => lives = value; }

        enum Dir { Up, Down, Left, Right }

        public void moveUp() { if (CanMove(Dir.Up)) this.PosY++; }
        public void moveDown() { if (CanMove(Dir.Down)) this.PosX++; }
        public void moveLeft() { if (CanMove(Dir.Left)) this.PosY--; }
        public void moveRight() { if (CanMove(Dir.Right)) this.PosY++; }
        private bool CanMove(Player.Dir d)
        {
            //TODO: logic
            return true;
        }
        public Player(int posX, int posY, int lives)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Lives = lives;
        }
    }

    public class Game
    {
        const int mapX = 10;
        const int mapY = 10;
        Tile[,] map = new Tile[10, 10];
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
                switch(Console.ReadKey().Key)
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
                }
            }
        }
        public void Draw()
        {
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    char c = (player.PosX == i && player.PosY == j) ? 'X' : map[i, j].representation;
                    Console.Write(c+ " ");
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