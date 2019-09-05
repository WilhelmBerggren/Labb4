using System;

namespace Labb4
{
    public class Game
    {
        const int mapHeight = 5;
        const int mapWidth = 5;
        Level level = new Level(mapHeight, mapWidth);
        Player player = new Player(2, 2, 10);

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
}