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
            this.player = new Player(2, 2, 0);
        }
        public void Start()
        {
            Console.WriteLine("Press enter to start!");
            Console.ReadLine();
            this.consoleWidth = Console.WindowWidth / 2;
            this.consoleHeight = Console.WindowHeight -2;
            this.level = new Level(consoleWidth, consoleHeight);
            Loop();
        }

        private void Loop()
        {
            while (player.Moves < 100)
            {
                Console.Clear();
                Draw();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        MovePlayer(0, -1);
                        break;
                    case ConsoleKey.A:
                        MovePlayer(-1, 0);
                        break;
                    case ConsoleKey.S:
                        MovePlayer(0, +1);
                        break;
                    case ConsoleKey.D:
                        MovePlayer(+1, 0);
                        break;
                }
            }
            Console.Write("You lose!");
            Console.ReadKey();
        }
        private void MovePlayer(int deltaX, int deltaY)
        {
            int targetX = player.PosX + deltaX;
            int targetY = player.PosY + deltaY;

            if (targetX >= 0 && targetX < level.map.GetLength(0) &&
                targetY >= 0 && targetY < level.map.GetLength(1))
            {
                player.move(level.map[targetX, targetY], deltaX, deltaY);
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
                    char c = level.map[x, y].representation;
                    if (player.PosX == x && player.PosY == y)
                        c = 'X';
                    if (DistanceFromPlayer(x, y) > 3)
                        c = ' ';
                    Console.Write(c + " ");
                }
            }
            Console.WriteLine($"Moves: {player.Moves}");
        }
    }
}
