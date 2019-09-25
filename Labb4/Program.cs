using System;

namespace Labb4
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Press 'L' in-game to view legend.\n\n" +
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
 * Nils: 5 rooms
 */
