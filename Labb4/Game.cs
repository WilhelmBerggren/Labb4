using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Labb4
{
    public class Game
    {
        private Player player;
        private Level level;
        private int mapWidth;
        private int mapHeight;
        private static int maxMovesAllowed;
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

        private readonly List<KeyValuePair<string, int>> scores;
        public Game()
        {
            this.scores = new List<KeyValuePair<string, int>>();
        }

        /*
         * Moved this.MapWidth and this.MapHeight to the method 'CheckConsoleWindowSize' to counteract the crash, previously occuring after step 2 below (first bug).
         * The second bug is NOT the same bug/error we prevented with this change.
         * (Revert this change to recreate that bug, follow the same steps as below)
         * 
         * Unfortunately we caused two other bugs:
         * 
         * First bug: Not printing walls/doors @ bottom row and right column. Only prints the top and the left lane,
         * as the right and the bottom are outside the windows frames, but the room size is still the same. You just change the windows size.
         * The draw-flicker also makes a comeback when this bug appears.
         * Also happens when switching from fullscreen-mode (and maximised) to windowed mode.
         * Solution? Redraw map but keep what is within the bounds of the windowsize? Have to get new values then and redraw just that.
         * If both doors are on the bottom or the right, we've got to fix that too.
         * 
         * Recreate the bug:
         * 1. Start the game (enter a key) without resizing the window. Leave the console window at the default size
         * 2. Let the map be drawn, then resize the screen below the accepted minimum value.
         *    This calls the CheckConsoleWindowSize() and resizes the screen to the minimum value !!AFTER!! you attempt to move below the min value
         *    And redraws the map with the flicker.
         * 3. Move the player in order to create the flickering effect. Pressing and holding a move-key yields more flickering.
         * 
         * 
         * Second bug: Game crashes due to an IndexOutOfBounds-exception in the method Draw().
         * The criminal in question is the line:
         * Tile currentTile = level.currentRoom.Map[row, column];
         * Solution? Any way to first check the size of the window and redraw according to that, without losing the other maps/rooms? Preventing cheating or loss.
         * 
         * Recreate the bug:
         * 1. Start the game with the window size being any size you want.
         * 2. Let the map be drawn.
         * 3. Enlargen the Console Window and attempt to move.
         */
        public void Start()
        {
            CheckConsoleWindowSize();

            maxMovesAllowed = 500;
            this.player = new Player(this, 2, 2, 0);
            //this.mapWidth = Console.WindowWidth / 2;
            //this.mapHeight = Console.WindowHeight - 1;
            this.level = new Level(this);
            GameLoop();
        }
       
        private void CheckConsoleWindowSize()
        {
            if (Console.WindowWidth < 88 || Console.WindowHeight < 10)
            {
                Console.SetWindowSize(88, 10);
            }
            this.mapWidth = Console.WindowWidth / 2;
            this.mapHeight = Console.WindowHeight - 2;
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
            Console.WriteLine("GAME OVER!\n");
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
            Console.WriteLine($"Name:\t\t\tUnique rooms:");

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
                name = "Anonymous";
            }
            else if (userInput.Length > 15)
            {
                name = userInput.Remove(14);
            }
            else
            {
                name = userInput;
            }

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
                $"Your moves starts at 0, with a max limit of {maxMovesAllowed}.\n" +
                $"Your max limit can both increase and decrease (more on this below)\n" +
                $"Your job is to attempt to go to as many new rooms as possible before your steps are up!\n\n" +
                "#: Wall Tiles. These are inaccessible and limits you to the game area\n\n" +
                $"M: Monster Tiles. Stepping on these will incur a penalty, reducing the max allowed steps by {MonsterMovesPenalty}\n\n" +
                $"T: Trap Tile. Stepping on these will incur a penalty, reducing the max allowed steps by {trapMovesPenalty}\n\n" +
                $"B: Button Tile. Stepping on these will increase the max allowed steps by {buttonMovesBoost}\n\n" +
                $"K: Key Tile. Stepping on these will unlock a door of the corresponding colour\n\n" +
                $"D: Door Tile. Will unlock and disappear once you pick up a key of the corresponding colour and\n" +
                $"leave behind an empty space, allowing you to pass into another room\n\n" +
                $"D (White): Returns you to the previous room from whence you came" +
                $"\n\n\nPress any key to return to the game.");
            Console.ReadKey();
            Console.Clear();
            Draw();
        }

        private double DistanceFromPlayer(int x, int y)
        {
            return Math.Sqrt(Math.Pow((player.PosX - x), 2) + Math.Pow((player.PosY - y), 2));
        }

        private void Draw()
        {
            for (int row = 0; row < mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    Tile currentTile = level.currentRoom.Map[row, column];
                    char c = currentTile.Representation;

                    if (DistanceFromPlayer(row, column) < 2.5)
                    {
                        currentTile.IsVisible = true;
                    }
                    if (!currentTile.IsVisible)
                    {
                        c = ' ';
                    }
                    if (player.PosX == row && player.PosY == column)
                    {
                        c = 'X';
                    }
                    PrintChar(c, currentTile.color, row, column);
                }
            }

            // \u2000 prints a half space, better suited than \t in this occasion.
            Console.Write($"\nMoves: {player.Moves}/{maxMovesAllowed}.\u2000" +
                $"Unique Rooms: {level.rooms.Count}.\u2000 " +
                "Press 'L' for legend.\u2000 " +
                "Press 'ESC' to quit.");
        }
        private void PrintChar(char c, ConsoleColor consoleColor, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x * 2, y);
                Console.ForegroundColor = consoleColor;
                Console.Write(c + " ");
            }
            catch (ArgumentOutOfRangeException) { }
        }
    }
}
