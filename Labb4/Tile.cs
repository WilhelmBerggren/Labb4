namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible to step on, meaning if you can go to its position
    public abstract class Tile
    {
        public char representation  { get; set; }
        public bool accessible { get; set; }
    }

    public class DoorTile : Tile
    {
        public DoorTile()
        {
            this.representation = 'D';
            this.accessible = true;
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
        public KeyTile()
        {
            this.representation = 'K';
            this.accessible = true;
        }
    }
}