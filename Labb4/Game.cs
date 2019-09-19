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
        private static int maxMovesAllowed = 500;
        private const int monsterMovesPenalty = 10;
        private const int trapMovesPenalty = 50;
        private const int buttonMovesBoost = 50;

        public int MapWidth { get => mapWidth; }
        public int MapHeight { get => mapHeight; }
        public Player Player { get => player; }
        public Level Level { get => level; }
        public static int MaxMovesAllowed { get => maxMovesAllowed; set => maxMovesAllowed = value; }
        public static int MonsterMovesPenalty { get => monsterMovesPenalty; }
        public static int TrapMovesPenalty { get => trapMovesPenalty; }
        public static int ButtonMovesBoost { get => buttonMovesBoost; }

        private List<KeyValuePair<string, int>> scores;

        public Game()
        {
            this.scores = new List<KeyValuePair<string, int>>();
        }

        public void Start()
        {
            CheckConsoleWindowSize();

            this.player = new Player(this, 2, 2, 0);
            this.mapWidth = Console.WindowWidth / 2;
            this.mapHeight = Console.WindowHeight - 2;
            this.level = new Level(this);
            GameLoop();
        }
       
        private void CheckConsoleWindowSize()
        {
            if (Console.WindowWidth < 88 || Console.WindowHeight < 10)
                Console.SetWindowSize(88, 10);
        }

        private void GameLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;
            while (player.Moves < maxMovesAllowed)
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
            Console.WriteLine("Insert name:");
            string userInput = Console.ReadLine();
            string name = ScoreNameCriterias(userInput, out name);

            Console.Clear();
            Console.WriteLine(
                "====================================\n" +
                "             SCORE LIST               " +
                "\n===================================="
                );

            scores.Add(new KeyValuePair<string, int>(name, level.rooms.Count));
            Console.WriteLine($"Name:\t\t\tFinal floor:");
            foreach (var i in scores.OrderByDescending(score => score.Value))
            {
                Console.WriteLine($"{i.Key}\t\t\t{i.Value}");
            }
            Console.WriteLine("\n\nPress any key to restart...");
            Console.ReadKey(true);
            Console.Clear();
            Start();
        }

        private string ScoreNameCriterias(string userInput, out string name)
        {
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                name = "Anonymous";

            else if (userInput.Length > 15)
                name = userInput.Remove(14);

            else
                name = userInput;
            return name;
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
                case ConsoleKey.L:
                    DisplayLegend();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
        private void DisplayLegend()
        {
            Console.Clear();
            Console.WriteLine(
                "#: Wall Tiles. These are inaccessible and limits you to the game area\n\n" +
                $"M: Monster Tiles. Stepping on these will incur a penalty, increase your steps by {MonsterMovesPenalty}\n\n" +
                $"T: Trap Tile. Stepping on these will incur a penalty, increase your steps by {trapMovesPenalty}\n\n" +
                $"B: Button Tile. Stepping on these will increase the max allowed steps by {buttonMovesBoost}\n\n" +
                $"K: Key Tile. Stepping on these will unlock a door of the corresponding colour\n\n" +
                $"D: Door Tile. Will unlock and disappear once you pick up a key of the corresponding colour and\n" +
                $"leaving behind an empty space, allowing you to step into a new room\n\n" +
                $"D (White): Returns you to a previous room" +
                $"\n\n\nPress any key to return to the game.");
            Console.ReadKey();
            Console.Clear();
            Draw();
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

                    CheckConsoleWindowSize();
                    Console.SetCursorPosition(row*2, column);
                    Console.ForegroundColor = currentTile.Color;
                    Console.Write(c + " ");
                }
            }
            Console.Write($"Moves: {player.Moves}, rooms: {level.rooms.Count}\t Press 'L' for legend at any time.\t Press 'ESC' to quit.");
        }
    }
}
