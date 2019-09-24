using System;

namespace Labb4
{
    public class Program
    {
        public static void Main()
        {
            Console.SetWindowSize(88, 10);

            Console.WriteLine("Movement keys: WASD\n" +
                "Press 'L' in-game for legend.\n\n" +
                "Press any key to start!");
            Console.ReadKey();

            Game game = new Game();
            game.Start();
        }
    }
}
/*
 * Highscore:
 * Wilhelm: 6 rooms
 * Simon: 5 rooms
 * Nils den Försck: 5 rooms
 */
