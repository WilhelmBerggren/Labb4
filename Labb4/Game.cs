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
        Button legend = new Button();

            public Game()
            {
                int windowWidth = 800;
                int windowHeight = 450;
                this.Size = new Size(windowWidth, windowHeight);
                this.Paint += Draw;
                int tilesWidth = windowWidth / tileSize - 2;
                int tilesHeight = windowHeight / tileSize - 1;
                this.level = new Level(tilesWidth, tilesHeight);

                this.KeyPreview = true;
                this.KeyPress +=
                    new KeyPressEventHandler(HandleKeypress);

                this.Controls.Add(legend);
                legend.Size = new Size(60, 40);
                legend.Location = new Point(700, 350);
                legend.Text = "Legend";
                legend.Click += new EventHandler(legendClick);
            }
        private void legendClick(object sender, EventArgs e)
        {
            MessageBox.Show(LegendInfo());
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
                case 'l':
                    MessageBox.Show(LegendInfo());
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

        private string LegendInfo()
        {
               return "Inaccessible Tiles:\n" +
               "#: Wall Tiles\n" +
               "O: Corner Tiles\n" +
               "\nAccessilbe Tiles:\n" +
               "K: Key Tiles\n" +
               "T: Trap Tiles\n" +
               "B: Button Tiles\n" +
               "M: Monster Tiles\n" +
               "\nConditional Tiles:\n" +
               "D: Door Tiles\n" +
               "\nCollect keys to unlock doors and walk on buttons to deactivate traps." +
               "\nStepping on monster tiles or trap tiles will add 10 moves." +
               "\n\nFinish the game by going through the door." +
               "\nTry to finish the game in as few moves as possible!";
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            int step = tileSize; //distance between the rows and columns
            int width = tileSize - 10; //the width of the rectangle
            int height = tileSize - 10; //the height of the rectangle

            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(SystemColors.Control); //Clear the draw area

                using (Font font1 = new Font("Arial", 28, FontStyle.Bold, GraphicsUnit.Point))
                {
                    for (int x = 0; x < level.mapWidth; x++)
                    {
                        for (int y = 0; y < level.mapHeight; y++)
                        {
                            char c = ' ';
                            if (DistanceFromPlayer(x, y, player) == 0)
                            {
                                c = 'X';
                            }
                            else if (DistanceFromPlayer(x, y, player) < 2)
                            {
                                c = level.map[x, y].representation;
                            }
                            Rectangle rect = new Rectangle(new Point(5 + step * x, 5 + step * y), new Size(width, height));
                            g.DrawString(c.ToString(), font1, Brushes.Blue, rect);
                        }
                    }
                    Font movesFont = new Font("Arial", 15, FontStyle.Bold, GraphicsUnit.Point);
                    Rectangle moves = new Rectangle(new Point(tileSize * level.mapWidth, 10), new Size(80, 20));
                    g.DrawString("Moves:", movesFont, Brushes.Blue, moves);
                    Rectangle movesCount = new Rectangle(new Point(tileSize * level.mapWidth, 30), new Size(80, 20));
                    g.DrawString($"{player.Moves}", movesFont, Brushes.Blue, movesCount);
                }
            }
        }

        static int DistanceFromPlayer(int x, int y, Player player)
        {
            return (int) Math.Sqrt(Math.Pow((player.PosX - x), 2) + Math.Pow((player.PosY - y), 2));
        }

        [STAThread]
        public void Start()
        {
            Application.Run(new Game());
        }
    }
}