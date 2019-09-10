using System;
using System.Drawing;
using System.Windows.Forms;

namespace Labb4
{
    public partial class Game : Form
    {
        const int tileSize = 50;
        Level level;
        Player player = new Player(2, 2, 10);

        public Game()
        {
            int windowWidth = 800;
            int windowHeight = 450;
            this.Size = new Size(windowWidth, windowHeight);
            this.Paint += Draw;
            int tilesWidth = windowWidth / tileSize - 1;
            int tilesHeight = windowHeight / tileSize - 1;
            this.level = new Level(tilesWidth, tilesHeight);

            this.KeyPreview = true;
            this.KeyPress +=
                new KeyPressEventHandler(HandleKeypress);
        }

        private void HandleKeypress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    MovePlayer(0, -1);
                    break;
                case 'a':
                    MovePlayer(-1, 0);
                    break;
                case 's':
                    MovePlayer(0, +1);
                    break;
                case 'd':
                    MovePlayer(+1, 0);
                    break;
                case '.':
                    Environment.Exit(0);
                    break;
            }
            this.Invalidate();
        }

        private void MovePlayer(int deltaX, int deltaY)
        {
            int targetX = player.PosX + deltaX;
            int targetY = player.PosY + deltaY;

            if (targetX >= 0 && targetX < level.map.GetLength(0) &&
                targetY >= 0 && targetY < level.map.GetLength(1))
            {
                player.move(level.map[targetX, targetY], deltaX, deltaY);
            }

        }

        private void Draw(object sender, PaintEventArgs e)
        {
            int step = tileSize; //distance between the rows and columns
            int width = tileSize - 10; //the width of the rectangle
            int height = tileSize - 10; //the height of the rectangle

            using (Graphics g = this.CreateGraphics())
            {
                //g.Clear(SystemColors.Control); //Clear the draw area
                using (Font font1 = new Font("Arial", 28, FontStyle.Bold, GraphicsUnit.Point))
                {
                    for (int x = 0; x < level.mapWidth; x++)
                    {
                        for (int y = 0; y < level.mapHeight; y++)
                        {
                        char c = (player.PosX == x && player.PosY == y) ? 'X' : level.map[x, y].representation;
                            Rectangle rect = new Rectangle(new Point(5 + step * x, 5 + step * y), new Size(width, height));
                            //g.DrawRectangle(pen, rect);
                            //g.FillRectangle(Brushes.Red, rect);
                            g.DrawString(c.ToString(), font1, Brushes.Blue, rect);
                        }
                    }
                    Rectangle rect2 = new Rectangle(new Point(0, tileSize * level.mapHeight), new Size(100, 50));
                    g.DrawString("Lives: 10", font1, Brushes.Blue, rect2);
                }
            }
        }

        [STAThread]
        public void Start()
        {
            Application.Run(new Game());
        }
    }
}