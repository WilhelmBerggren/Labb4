using System;

namespace Labb4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adjust window to change map size and press any key to start!");
            Console.ReadKey();
            Game game = new Game();
            game.Start();
        }
    }
}
/*
 * You start in a room and have to collect a key while avoiding monsters and go onto a door-tile.
 * Upon stepping on the door-tile, you end up in a smaller, rectangular room with a treasure chest.
 * If key is collected, enter the room. If key is NOT collected, display message.
 * 
 * WIN-Condition: Open the chest to win.
 * LOSE-Condition: Lose upon reaching 100 steps.
 * 
 * TODO: 
 * Fix bug:
 * When launching the game at a size larger than the CheckConsoleWindowSize methods if-parameters(game class)
 * the game crashes when attempting to redraw it after reducing the size below the if-parameters.
 * If launching the game below the if-parameters range, checking it before redrawing increases the size to the if-parameters
 *
 * 
 */


    /*
       interface, implements all chests.
       What the chests drop and all, is done with the interface

       Interface = treasurechest
       firechest = fire items
       ice chest, ice items.
    */