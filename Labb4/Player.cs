namespace Labb4
{
    public class Player
    {
        public int PosX;
        public int PosY;
        private int moves;
        private Game game;

        public Player(Game game, int posX, int posY, int moves)
        {
            this.game = game;
            this.PosX = posX;
            this.PosY = posY;
            this.moves = moves;
        }

        public int Moves { get => moves; set => moves = (moves > 0) ? value : moves; }

        public void Move(int deltaX, int deltaY)
        {
            int targetX = PosX + deltaX;
            int targetY = PosY + deltaY;

            if (targetX >= 0 && targetX < game.MapWidth &&
                targetY >= 0 && targetY < game.MapHeight)
            {
                Tile currentTile = game.Level.currentRoom.Map[targetX, targetY];
                if (currentTile == null || !currentTile.isAccessible)
                {
                    return;
                }

                this.PosX += deltaX;
                this.PosY += deltaY;
                this.moves++;

                HandleCollision(currentTile);
            }

        }

        public void HandleCollision(Tile collidingTile)
        {
            if (collidingTile is ITileCollision)
            {
                var tile = (ITileCollision)collidingTile;
                tile.Collide(game);
            }
        }
    }
}