using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb4
{
    public class Game
    {
        private Player player;
        private Level level;
        private int mapWidth;
        private int mapHeight;

        public int MapWidth { get => mapWidth; }
        public int MapHeight { get => mapHeight; }
        public Player Player { get => player; }
        public Level Level { get => level; }

        private List<KeyValuePair<string, int>> scores;

        public Game()
        {
            this.player = new Player(this, 2, 2, 0);
            this.scores = new List<KeyValuePair<string, int>>();
        }

        public void Start()
        {
            Console.WriteLine("Press enter to start!");
            Console.ReadLine();
            this.mapWidth = Console.WindowWidth / 2;
            this.mapHeight = Console.WindowHeight - 1;
            this.level = new Level(this);
            GameLoop();
        }

        private void GameLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;
            while (player.Moves < 100)
            {
                Draw();
                WaitForInput();
            }
            Console.Clear();
            GameOver();
            Console.ReadKey();
        }

        private void GameOver()
        {
            Console.WriteLine("insert name:");
            string name = Console.ReadLine();
            if (name == null)
                name = "anonymous";
            scores.Add(new KeyValuePair<string, int>(name, level.rooms.Count));

            foreach(var i in scores.OrderByDescending(score => score.Value))
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }
            Console.WriteLine("Press enter to restart");
            Console.ReadKey(true);
            Console.Clear();
            Start();
        }

        private void WaitForInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    player.Move(0, -1);
                    break;
                case ConsoleKey.A:
                    player.Move(-1, 0);
                    break;
                case ConsoleKey.S:
                    player.Move(0, +1);
                    break;
                case ConsoleKey.D:
                    player.Move(+1, 0);
                    break;
            }
        }
        private int DistanceFromPlayer(int x, int y)
        {
            return (int)Math.Sqrt(Math.Pow((player.PosX - x), 2) + Math.Pow((player.PosY - y), 2));
        }
        private void Draw()
        {
            for (int row = 0; row < mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    Tile currentTile = level.currentRoom.Map[row, column];
                    char c = currentTile.Representation;
                    if (DistanceFromPlayer(row, column) > 3)
                        c = ' ';
                    if (row == 0 || row == mapWidth - 1 ||
                        column == 0 || column == mapHeight - 1)
                        c = level.currentRoom.Map[row, column].Representation;
                    if (player.PosX == row && player.PosY == column)
                        c = 'X';

                    Console.SetCursorPosition(row*2, column);
                    Console.ForegroundColor = currentTile.Color;
                    Console.Write(c + " ");
                }
            }
            Console.Write($"Moves: {player.Moves}, rooms: {level.rooms.Count}");
        }
    }
}
