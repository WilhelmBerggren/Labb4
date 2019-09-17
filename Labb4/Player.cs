namespace Labb4
{
    public class Player
    {
        public Level level;
        public int PosX;
        public int PosY;
        private int moves;

        public Player(Level level, int posX, int posY, int moves)
        {
            this.level = level;
            this.PosX = posX;
            this.PosY = posY;
            this.moves = moves;
        }

        public int Moves { get => moves; set => moves = (moves > 0) ? value : moves; }

        public void Move(Level level, int deltaX, int deltaY)
        {
            this.level = level;
            int targetX = PosX + deltaX;
            int targetY = PosY + deltaY;

            if (targetX >= 0 && targetX < level.map.GetLength(0) &&
                targetY >= 0 && targetY < level.map.GetLength(1))
            {
                Tile currentTile = level.map[targetX, targetY];
                if (currentTile == null || !currentTile.IsAccessible)
                    return;

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
                tile.Collide(this);
            }
        }
    }
}