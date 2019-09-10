namespace Labb4
{
    public class Player
    {
        public int PosX;
        public int PosY;
        private int moves;
        public Player(int posX, int posY, int moves)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.moves = moves;
        }

        public int Moves { get => moves; set => moves = (moves > 0) ? value : moves; }

        public void move(Tile t, int deltaX, int deltaY)
        {
            if (t.accessible)
            {
                this.PosX += deltaX;
                this.PosY += deltaY;
                this.moves++;
                if (t.GetType() == typeof(MonsterTile)) this.moves += 10; // Adds 10 moves if player steps on a monster tile.
            }
        }
    }
}