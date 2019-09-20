using System;

namespace Labb4
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Adjust window to change map size and press any key to start!");
            Console.ReadKey();
            Game game = new Game();
            game.Start();
        }
    }
}
/*
 * Probably missed some coding conventions.
 * Code can still be refreshed by removing duplicate code, alternatively refactor to reduce code overall.
 * Maybe add a random starting place for the player?
 * Finally, before handing this in, we have to recheck the project to see if we can rewrite some things.
 * 
 * Står i bedömningskriterierna för G att det ska gå att klara spelet och se sin poäng.
 * Om vi inte behöver uppfylla alla G-kriterier så kan vi bortse ifrån denna. Då blir det bara som ett race helt enkelt, där man ser vem som kan komma längst.
 * Fråga Pontus om detta är OK?
 * 
 * Lacks a WIN-condition (see above)
 * LOSE-condition: GAME OVER occurs when the amount of player steps equals the max allowed steps.
 * 
 * TODO: 
 * Fix bugs:
 * In the starting room, there are no tiles whatsoever on the row or column where the player starts.
 * Ex. player(2, 2, 0) = third row, third column (counting walls, arrays are 0-based). Nothing appears on either lines.
 * Two bugs listed in the Game-Class (just above the Start() method), deals with resizing and printing the map and errors/crashes.
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
