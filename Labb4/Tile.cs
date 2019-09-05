namespace Labb4
{
    // class Tile contains representation (char) holds just one sign.
    // bool accessible to check if it's accessible to step on, meaning if you can go to its position
    public class Tile
    {
        public char representation;
        public bool accessible;
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
}