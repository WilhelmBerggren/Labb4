using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Labb4
{
    public partial class Class1 : Form
    {
        void Class1Keypress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    player.move(level.map[player.PosX - 1, player.PosY], -1, 0);
                    break;
                case 'a':
                    player.move(level.map[player.PosX, player.PosY - 1], 0, -1);
                    break;
                case 's':
                    player.move(level.map[player.PosX + 1, player.PosY], +1, 0);
                    break;
                case 'd':
                    player.move(level.map[player.PosX, player.PosY + 1], 0, +1);
                    break;
                case '.':
                    Environment.Exit(0);
                    break;
            }
            this.Refresh();
        }

        public Class1(Level level, Player player)
        {
            this.Paint += Draw2DArray;
            this.level = level;
            this.player = player;

            this.KeyPreview = true;
            this.KeyPress +=
                new KeyPressEventHandler(Class1Keypress);
        }

        Level level;
        Player player;

        private void Draw2DArray(object sender, PaintEventArgs e)
        {
            int step = 50; //distance between the rows and columns
            int width = 40; //the width of the rectangle
            int height = 40; //the height of the rectangle

            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(SystemColors.Control); //Clear the draw area
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    for (int x = 0; x < level.mapWidth; x++)
                    {
                        for (int y = 0; y < level.mapHeight; y++)
                        {
                            char c = (player.PosX == x && player.PosY == y) ? 'X' : level.map[x, y].representation;
                            using (Font font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point))
                            {
                                Rectangle rect = new Rectangle(new Point(5 + step * y, 5 + step * x), new Size(width, height));
                                g.DrawRectangle(pen, rect);
                                g.FillRectangle(Brushes.Red, rect);
                                g.DrawString(c.ToString(), font1, Brushes.Blue, rect);
                            }
                        }
                    }
                }
            }
        }
    }


}