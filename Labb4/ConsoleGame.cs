using System;

namespace Labb4
{
    class ConsoleGame
    {
        private Player player;
        private Level level;
        int consoleWidth;
        int consoleHeight;
        public ConsoleGame()
        {
            this.player = new Player(level, 2, 2, 0);
        }
        public void Start()
        {
            Console.WriteLine("Press enter to start!");
            Console.ReadLine();
            this.consoleWidth = Console.WindowWidth / 2;
            this.consoleHeight = Console.WindowHeight - 1;
            this.level = new Level(consoleWidth, consoleHeight);
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
            Console.Write("You lose!");
            Console.ReadKey();
        }
        private void WaitForInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    player.Move(level, 0, -1);
                    break;
                case ConsoleKey.A:
                    player.Move(level, -1, 0);
                    break;
                case ConsoleKey.S:
                    player.Move(level, 0, +1);
                    break;
                case ConsoleKey.D:
                    player.Move(level, +1, 0);
                    break;
            }
        }
        private int DistanceFromPlayer(int x, int y)
        {
            return (int)Math.Sqrt(Math.Pow((player.PosX - x), 2) + Math.Pow((player.PosY - y), 2));
        }
        private void Draw()
        {
            int mapWidth = level.map.GetLength(0);
            int mapHeight = level.map.GetLength(1);

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Tile currentTile = level.map[x, y];
                    char c = currentTile.Representation;
                    if (DistanceFromPlayer(x, y) > 3)
                        c = ' ';
                    if (x == 0 || x == mapWidth - 1 ||
                        y == 0 || y == mapHeight - 1)
                        c = level.map[x, y].Representation;
                    if (player.PosX == x && player.PosY == y)
                        c = 'X';

                    Console.SetCursorPosition(x*2, y);
                    Console.ForegroundColor = currentTile.Color;
                    Console.Write(c + " ");
                }
            }
            Console.Write($"Moves: {player.Moves}");
        }
    }
}
