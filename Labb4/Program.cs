using System;
using System.Windows.Forms;

namespace Labb4
{
    class Program
    {
        static void Main(string[] args)
        {
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
 * Same color for key and door.
 * 
 * Implement an array to store maps (max 3 maps)
 * Start at index 0 (first map), when you enter the door in map 1, you go to map 2. Chest at last map.
 * 
 * Implement feature to keep drawing tiles the player has already walked on.
 * Implement feature to display highscore of number of moves when finishing game.
 *      When finishing the game, display popup to grats user and restart. Save highscore.
 * 
 */


    /*
       interface, implements all chests.
       What the chests drop and all, is done with the interface

       Interface = treasurechest
       firechest = fire items
       ice chest, ice items.
    */