﻿namespace Labb4
{
    public class Player
    {
        public int PosX;
        public int PosY;
        public int lives;
        public Player(int posX, int posY, int lives)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.lives = lives;
        }

        public void move(Tile t, int deltaX, int deltaY)
        {
            if (t.accessible)
            {
                this.PosX += deltaX;
                this.PosY += deltaY;
                if (t.GetType() == typeof(MonsterTile)) lives--;
            }
        }
    }
}