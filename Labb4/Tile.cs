using System;

namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible to step on, meaning if you can go to its position
    public abstract class Tile
    {
        public char representation  { get; set; }
        public bool isAccessible { get; set; }
        public bool isVisible { get; set; }
        public ConsoleColor color = ConsoleColor.White;
    }

    public class DoorTile : Tile, ITileCollision
    {
        public Room leadsTo;
        public DoorTile(Game game, ConsoleColor color, Room currentRoom)
        {
            this.color = color;
            this.representation = 'D';
            this.isAccessible = false;
            this.leadsTo = new Room(game, currentRoom);
            this.isVisible = true;
        }

        public void Collide(Game game)
        {
            game.Level.EnterRoom(leadsTo);
        }
    }

    public class ReturnDoorTile : Tile, ITileCollision
    {
        public Room leadsTo;
        public ReturnDoorTile(Room previousRoom)
        {
            this.leadsTo = previousRoom;
            this.color = ConsoleColor.White;
            this.representation = 'D';
            this.isAccessible = true;
            this.isVisible = true;
        }

        public void Collide(Game game)
        {
            game.Level.EnterRoom(leadsTo);
        }
    }

    public class ButtonTile : Tile, ITileCollision
    {
        public bool buttonIsActive = true;
        public TrapTile TrapTile { get; set; }
        public ButtonTile(TrapTile trapTile)
        {
            this.representation = 'B';
            this.isAccessible = true;
            this.TrapTile = trapTile;
            this.color = ConsoleColor.Cyan;
            this.TrapTile.color = this.color;
        }

        public void Collide(Game game)
        {
            this.color = ConsoleColor.White; //this.color = ButtonTile's color
            TrapTile.color = ConsoleColor.White;
            representation = '.';
            TrapTile.trapIsActive = false;
            TrapTile.representation = '.';
            if (buttonIsActive)
            {
                Game.MaxMovesAllowed += Game.ButtonMovesBoost;
                buttonIsActive = false;
            }
        }
    }

    public class WallTile : Tile
    {
        public WallTile()
        {
            this.representation = '#';
            this.isAccessible = false;
            this.isVisible = true;
        }
    }

    public class CornerTile : Tile
    {
        public CornerTile()
        {
            this.representation = '#';
            this.isVisible = true;
        }
    }

    public class RoomTile : Tile
    {
        public RoomTile(ConsoleColor color = ConsoleColor.White)
        {
            this.representation = '.';
            this.isAccessible = true;
            this.color = color;
        }
    }

    public class MonsterTile : Tile, ITileCollision
    {
        public MonsterTile()
        {
            this.representation = 'M';
            this.isAccessible = true;
            this.color = ConsoleColor.Green;
        }

        public void Collide(Game game)
        {
            Game.MaxMovesAllowed -= Game.MonsterMovesPenalty;
        }
    }

    public class KeyTile : Tile, ITileCollision
    {
        public DoorTile doorTile { get; set; }
        public KeyTile(DoorTile doorTile)
        {
            this.doorTile = doorTile;
            this.representation = 'K';
            this.isAccessible = true;
            this.color = this.doorTile.color;
        }

        public void Collide(Game game)
        {
            representation = new RoomTile().representation;
            color = new RoomTile().color;
            doorTile.isAccessible = true;
            doorTile.representation = ' ';
        }
    }

    public class TrapTile : Tile, ITileCollision
    {
        public bool trapIsActive;
        public TrapTile()
        {
            this.trapIsActive = true;
            this.representation = 'T';
            this.isAccessible = true;
        }

        public void Collide(Game game)
        {
            if (trapIsActive)
            {
                Game.MaxMovesAllowed -= Game.TrapMovesPenalty;
            }
        }
    }
}