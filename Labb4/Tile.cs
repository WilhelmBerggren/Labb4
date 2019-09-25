using System;

namespace Labb4
{
    public abstract class Tile
    {
        public char Representation  { get; set; }
        public bool IsAccessible { get; set; }
        public bool IsVisible { get; set; }
        public ConsoleColor color = ConsoleColor.White;
    }

    public class WallTile : Tile
    {
        public WallTile()
        {
            this.Representation = '#';
            this.IsAccessible = false;
            this.IsVisible = true;
        }
    }

    // Different tile with same representation as WallTile
    // to prevent any tiles from ending up in the corners
    public class CornerTile : Tile
    {
        public CornerTile()
        {
            this.Representation = '#';
            this.IsVisible = true;
        }
    }

    public class RoomTile : Tile
    {
        public RoomTile(ConsoleColor color = ConsoleColor.White)
        {
            this.Representation = '.';
            this.IsAccessible = true;
            this.color = color;
        }
    }

    public class DoorTile : Tile, ITileCollision
    {
        private readonly Room leadsTo;

        public DoorTile(Game game, ConsoleColor color)
        {
            this.color = color;
            this.Representation = 'D';
            this.IsAccessible = false;
            this.leadsTo = new Room(game);
            this.IsVisible = true;
        }

        public void Collide(Game game)
        {
            game.Level.EnterRoom(leadsTo);
        }
    }

    public class ReturnDoorTile : Tile, ITileCollision
    {
        private readonly Room leadsTo;

        public ReturnDoorTile(Room previousRoom)
        {
            this.leadsTo = previousRoom;
            this.color = ConsoleColor.White;
            this.Representation = 'D';
            this.IsAccessible = true;
            this.IsVisible = true;
        }

        public void Collide(Game game)
        {
            game.Level.EnterRoom(leadsTo);
        }
    }

    public class KeyTile : Tile, ITileCollision
    {
        private readonly DoorTile PairedDoor;

        public KeyTile(DoorTile doorTile)
        {
            this.PairedDoor = doorTile;
            this.Representation = 'K';
            this.IsAccessible = true;
            this.color = this.PairedDoor.color;
        }

        public void Collide(Game game)
        {
            Representation = new RoomTile().Representation;
            color = new RoomTile().color;
            PairedDoor.IsAccessible = true;
            PairedDoor.Representation = ' ';
        }
    }

    public class MonsterTile : Tile, ITileCollision
    {
        public MonsterTile()
        {
            this.Representation = 'M';
            this.IsAccessible = true;
            this.color = ConsoleColor.Green;
        }

        public void Collide(Game game)
        {
            game.MaxMovesAllowed -= Game.MonsterMovesPenalty;
        }
    }

    public class TrapTile : Tile, ITileCollision
    {
        public bool trapIsActive;

        public TrapTile()
        {
            this.trapIsActive = true;
            this.Representation = 'T';
            this.IsAccessible = true;
        }

        public void Collide(Game game)
        {
            if (trapIsActive)
            {
                game.MaxMovesAllowed -= Game.TrapMovesPenalty;
            }
        }
    }

    public class ButtonTile : Tile, ITileCollision
    {
        private bool buttonIsActive = true;
        public TrapTile TrapTile { get; set; }

        public ButtonTile(TrapTile trapTile)
        {
            this.Representation = 'B';
            this.IsAccessible = true;
            this.TrapTile = trapTile;
            this.color = ConsoleColor.Cyan;
            this.TrapTile.color = this.color;
        }

        public void Collide(Game game)
        {
            // Sets representations and color to match RoomTile.
            this.color = ConsoleColor.White;
            TrapTile.color = ConsoleColor.White;
            Representation = '.';
            TrapTile.trapIsActive = false;
            TrapTile.Representation = '.';

            if (buttonIsActive)
            {
                game.MaxMovesAllowed += Game.ButtonMovesBoost;
                buttonIsActive = false;
            }
        }
    }
}