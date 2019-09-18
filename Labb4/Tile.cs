using System;

namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible to step on, meaning if you can go to its position
    public abstract class Tile
    {
        public char Representation  { get; set; }
        public bool IsAccessible { get; set; }
        public ConsoleColor Color = ConsoleColor.White;
    }

    public class DoorTile : Tile, ITileCollision
    {
        public Room previousRoom;
        public DoorTile(ConsoleColor color)
        {
            this.Color = color;
            this.Representation = 'D';
            this.IsAccessible = false;
        }

        public void Collide(Game game)
        {
            game.Level.EnterRoom(this, previousRoom);
        }
    }

    public class ReturnDoorTile : Tile, ITileCollision
    {
        public Room previousRoom;
        public ReturnDoorTile(Room room)
        {
            this.previousRoom = room;
            this.Color = ConsoleColor.White;
            this.Representation = 'D';
            this.IsAccessible = true;
        }

        public void Collide(Game game)
        {
            game.Level.EnterRoom(null, previousRoom);
        }
    }

    public class ButtonTile : Tile, ITileCollision
    {
        public TrapTile trapTile { get; set; }
        public ButtonTile(TrapTile trapTile)
        {
            this.Representation = 'B';
            this.IsAccessible = true;
            this.trapTile = trapTile;
            this.Color = ConsoleColor.Cyan;
            this.trapTile.Color = this.Color;
        }

        public void Collide(Game game)
        {
            Representation = '.';
            trapTile.active = false;
            trapTile.Representation = '.';
        }
    }

    public class WallTile : Tile
    {
        public WallTile()
        {
            this.Representation = '#';
            this.IsAccessible = false;
        }
    }

    public class CornerTile : Tile
    {
        public CornerTile()
        {
            this.Representation = '#';
        }
    }

    public class RoomTile : Tile
    {
        public RoomTile(ConsoleColor color = ConsoleColor.White)
        {
            this.Representation = '.';
            this.IsAccessible = true;
            this.Color = color;
        }
    }

    public class MonsterTile : Tile, ITileCollision
    {
        public MonsterTile()
        {
            this.Representation = 'M';
            this.IsAccessible = true;
            this.Color = ConsoleColor.Green;
        }

        public void Collide(Game game)
        {
            game.Player.Moves += 10;
        }
    }

    public class KeyTile : Tile, ITileCollision
    {
        public DoorTile doorTile { get; set; }
        public KeyTile(DoorTile doorTile)
        {
            this.doorTile = doorTile;
            this.Representation = 'K';
            this.IsAccessible = true;
            this.Color = this.doorTile.Color;
        }

        public void Collide(Game game)
        {
            Representation = new RoomTile().Representation;
            Color = new RoomTile().Color;
            doorTile.IsAccessible = true;
            doorTile.Representation = ' ';
        }
    }

    public class TrapTile : Tile, ITileCollision
    {
        public bool active;
        public TrapTile()
        {
            this.active = true;
            this.Representation = 'T';
            this.IsAccessible = true;
        }

        public void Collide(Game game)
        {
            if(active)
                game.Player.Moves += 10;
        }
    }
}