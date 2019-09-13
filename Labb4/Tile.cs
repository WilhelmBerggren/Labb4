using System.Drawing;

namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible to step on, meaning if you can go to its position
    public abstract class Tile
    {
        public char representation  { get; set; }
        public bool accessible { get; set; }
        public Brush color { get; set; }
    }

    public class DoorTile : Tile
    {
        public DoorTile()
        {
            this.representation = 'D';
            this.accessible = false;
            this.color = Brushes.Black;
        }
    }

    public class ButtonTile : Tile
    {
        public TrapTile trapTile { get; set; }
        public ButtonTile(TrapTile trapTile)
        {
            this.representation = 'B';
            this.accessible = true;
            this.trapTile = trapTile;
        }
    }

    public class WallTile : Tile
    {
        public WallTile()
        {
            this.representation = '#';
            this.accessible = false;
        }
    }

    public class CornerTile : Tile
    {
        public CornerTile()
        {
            this.representation = 'O';
            this.accessible = false;
        }
    }

    public class RoomTile : Tile
    {
        public RoomTile()
        {
            this.representation = '.';
            this.accessible = true;
        }
    }

    public class MonsterTile : Tile
    {
        public MonsterTile()
        {
            this.representation = 'M';
            this.accessible = true;
        }
    }

    public class KeyTile : Tile
    {
        public DoorTile doorTile { get; set; }
        public KeyTile(DoorTile doorTile)
        {
            this.doorTile = doorTile;
            this.representation = 'K';
            this.accessible = true;
        }
    }

    public class TrapTile : Tile
    {
        public bool active;
        public TrapTile()
        {
            this.active = true;
            this.representation = 'T';
            this.accessible = true;
        }
    }
}