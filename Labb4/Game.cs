using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb4
{
    public class Game
    {
        private const int monsterMovesPenalty = 15;
        private const int trapMovesPenalty = 50;
        private const int buttonMovesBoost = 10;

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public Player Player { get; private set; }
        public Level Level { get; private set; }
        public int MaxMovesAllowed { get; set; }
        public static int MonsterMovesPenalty { get => monsterMovesPenalty; }
        public static int TrapMovesPenalty { get => trapMovesPenalty; }
        public static int ButtonMovesBoost { get => buttonMovesBoost; }

        // Using a List of KVP instead of a Dictionary to allow multiple entries of the same key.
        private readonly List<KeyValuePair<string, int>> scores;

        public Game()
        {
            this.scores = new List<KeyValuePair<string, int>>();
            scores.Add(new KeyValuePair<string, int>("Simon", 5));
            scores.Add(new KeyValuePair<string, int>("wil", 6));
            scores.Add(new KeyValuePair<string, int>("Nils", 5));
        }

        public void Start()
        {
            Console.SetWindowSize(88, 10);

            this.MaxMovesAllowed = 200;
            this.Player = new Player(this, 2, 2, 0);
            this.MapWidth = Console.WindowWidth / 2;
            this.MapHeight = Console.WindowHeight - 1;
            this.Level = new Level(this);
            GameLoop();
        }

        private void GameLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;

            while (Player.Moves < MaxMovesAllowed)
            {
                Draw();
                WaitForInput();
            }

            GameOver();
            Console.ReadKey();
        }

        private void WaitForInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    Player.Move(0, -1);
                    break;
                case ConsoleKey.A:
                    Player.Move(-1, 0);
                    break;
                case ConsoleKey.S:
                    Player.Move(0, +1);
                    break;
                case ConsoleKey.D:
                    Player.Move(+1, 0);
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
            Console.SetWindowSize(120, 30);
            Console.Clear();
            Console.WriteLine(
                "Movement keys: WASD\n\n" +
                $"Your moves starts at 0, with a max limit of {MaxMovesAllowed}.\n" +
                $"Your max limit can both increase and decrease (more on this below)\n" +
                $"Your job is to attempt to go to as many new rooms as possible before your steps are up!\n\n\n" +
                "X: Player position\n\n" +
                "#: Wall Tiles. These are inaccessible and limits you to the game area\n\n" +
                $"M: Monster Tiles. Stepping on these will incur a penalty, reducing the max allowed steps by {MonsterMovesPenalty}\n\n" +
                $"T: Trap Tile. Stepping on these will incur a penalty, reducing the max allowed steps by {trapMovesPenalty}\n\n" +
                $"B: Button Tile. Stepping on these will increase the max allowed steps by {buttonMovesBoost}\n" +
                $"and remove it's pair trap on the map. There will be multiple button-trap pairs.\n\n" +
                $"K: Key Tile. Stepping on these will unlock a door of the corresponding colour\n\n" +
                $"D: Door Tile. Will unlock and disappear once you pick up a key of the corresponding colour and\n" +
                $"leave behind an empty space, allowing you to pass into another room\n\n" +
                $"D (White): Returns you to the previous room from whence you came" +
                $"\n\n\n\nPress any key to return to the game.");
            Console.ReadKey();
            Console.Clear();
            Draw();
        }

        private void Draw()
        {
            KeepWindowSize();
            for (int row = 0; row < this.MapWidth; row++)
            {
                for (int column = 0; column < this.MapHeight; column++)
                {
                    Tile currentTile = Level.currentRoom.Map[row, column];
                    char c = currentTile.Representation;

                    if (DistanceFromPlayer(row, column) < 2.5)
                    {
                        currentTile.IsVisible = true;
                    }
                    if (!currentTile.IsVisible)
                    {
                        c = ' ';
                    }
                    if (Player.PosX == row && Player.PosY == column)
                    {
                        c = 'X';
                    }
                    PrintChar(c, currentTile.color, row, column);
                }
            }

            // \u2000 prints a half space, better suited than \t in this occasion.
            Console.Write($"\nMoves: {Player.Moves}/{MaxMovesAllowed}.\u2000" +
                $"Unique Rooms: {Level.rooms.Count}.\u2000 " +
                "Press 'L' for legend.\u2000 " +
                "Press 'ESC' to quit.");
        }

        private void KeepWindowSize()
        {
            if (Console.WindowWidth == 88 && Console.WindowHeight == 11)
            {
                return;
            }
            else
            { 
                Console.Clear();
                Console.SetWindowSize(88, 11);
            }
        }

        private double DistanceFromPlayer(int x, int y)
        {
            return Math.Sqrt(Math.Pow((Player.PosX - x), 2) + Math.Pow((Player.PosY - y), 2));
        }

        private void PrintChar(char c, ConsoleColor consoleColor, int x, int y)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x * 2, y);
            Console.ForegroundColor = consoleColor;
            Console.Write(c + " ");
        }

        private void GameOver()
        {
            Console.Clear();
            Console.SetWindowSize(120, 30);
            Console.WriteLine("GAME OVER!\n");
            Console.WriteLine("Insert name (max 7 chars):");
            string userInput = Console.ReadLine();
            string name = ScoreNameCriterias(userInput, out name);

            Console.Clear();
            Console.WriteLine(
                "====================================\n" +
                "             SCORE LIST               " +
                "\n===================================="
                );

            scores.Add(new KeyValuePair<string, int>(name, Level.rooms.Count));
            Console.WriteLine("Name:\t\t\t" +
                "Unique rooms:");

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
            {
                name = "Anon";
            }
            else if (userInput.Length > 7)
            {
                name = userInput.Remove(7);
            }
            else
            {
                name = userInput;
            }

            return name;
        }
    }
}
