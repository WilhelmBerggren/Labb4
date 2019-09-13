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

        public void move(Tile currentTile, int deltaX, int deltaY)
        {
            if (currentTile == null)
                return;
            if (currentTile.accessible)
            {
                this.PosX += deltaX;
                this.PosY += deltaY;
                this.moves++;

                if (currentTile.GetType() == typeof(MonsterTile)) this.moves += 10;
                else if (currentTile.GetType() == typeof(TrapTile))
                {
                    TrapTile tt = (TrapTile)currentTile;
                    if (tt.active)
                        this.moves += 10;
                }
                else if (currentTile.GetType() == typeof(KeyTile))
                {
                    KeyTile keyTile = (KeyTile)currentTile;
                    currentTile.representation = '.';
                    keyTile.doorTile.accessible = true;
                    keyTile.doorTile.representation = ' ';
                }
                else if (currentTile.GetType() == typeof(ButtonTile))
                {
                    ButtonTile trapTile = (ButtonTile)currentTile;
                    currentTile.representation = '.';
                    trapTile.trapTile.active = false;
                    trapTile.trapTile.representation = '.';

                }
            }
        }
    }
}