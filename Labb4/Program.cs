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
 * LOSE-Condition: Step on monsters enough times to reduce life to 0. Each step on a monster-tile reduces HP by 1.
 * 
 * TODO: 
 * Implement and place two keys of separate colours.
 * Implement and place two door-tiles of separate colours
 *      and if-statements to see if the correct key is collected to open that specific door.
 *      
 * Implement an empty map except for the player and the walls.
 * Set the different tiles (such as door, keys, monsters) to only appear once the player is next to them, as per view distance.
 * Implement view distance to just a square around the player until a torch is collected.
 * Implement and place a torch. If torch is collected, change draw function to increase viewdistance
 * Randomize placement of torch, keys, doors, monsters and players.
 */


    /*
       interface, implements all chests.
       What the chests drop and all, is done with the interface

       Interface = treasurechest
       firechest = fire items
       ice chest, ice items.
    */